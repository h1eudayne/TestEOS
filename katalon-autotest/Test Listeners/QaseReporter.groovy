import com.kms.katalon.core.annotation.BeforeTestCase
import com.kms.katalon.core.annotation.BeforeTestSuite
import com.kms.katalon.core.annotation.AfterTestCase
import com.kms.katalon.core.annotation.AfterTestSuite
import com.kms.katalon.core.context.TestSuiteContext
import com.kms.katalon.core.context.TestCaseContext
import groovy.json.JsonOutput
import groovy.json.JsonSlurper

class QaseReporter {

    static final String QASE_API_TOKEN = System.getenv('QASE_API_TOKEN') ?: 'f3ce87c9cbb8a008076ca920fefd486545cdd83f63384a90239b30f09aaae951'
    static final String QASE_PROJECT_CODE = 'EOS'
    static final String QASE_API_BASE = 'https://api.qase.io/v1'
    
    static final Map<String, Integer> QASE_CASE_MAPPING = [
        'EOS Test'       : 58,
        'EOS Login Test' : 59
    ]

    static int runId = 0
    static List<Map> results = []
    static boolean suiteMode = false

    @BeforeTestSuite
    def beforeTestSuite(TestSuiteContext context) {
        println "[QaseReporter] Starting test suite: ${context.getTestSuiteId()}"
        suiteMode = true
        results.clear()
        createQaseRun()
    }

    @BeforeTestCase
    def beforeTestCase(TestCaseContext context) {
        if (!suiteMode && runId == 0) {
            println "[QaseReporter] Running single test case - creating Qase run"
            createQaseRun()
        }
    }

    @AfterTestCase
    def afterTestCase(TestCaseContext context) {
        def testCaseName = context.getTestCaseId().split('/').last()
        def status = context.getTestCaseStatus()
        
        def qaseStatus = mapStatus(status)
        
        println "[QaseReporter] Test '${testCaseName}' finished: ${status} → Qase status: ${qaseStatus}"
        
        def caseId = QASE_CASE_MAPPING.get(testCaseName, 0)
        
        if (caseId == 0) {
            println "[QaseReporter] WARNING: No Qase case ID mapped for '${testCaseName}'. Skipping report."
            println "[QaseReporter] → Add mapping in QaseReporter.groovy: '${testCaseName}' : <qase_case_id>"
            return
        }
        
        if (runId == 0) {
            println "[QaseReporter] WARNING: No active Qase run. Skipping report."
            return
        }
        
        def resultBody = JsonOutput.toJson([
            case_id   : caseId,
            status    : qaseStatus,
            time_ms   : context.getTestCaseVariables().get('executionTime', 0),
            comment   : "Katalon automated test: ${testCaseName}",
            stacktrace: status == 'FAILED' ? context.getMessage() : null
        ])
        
        try {
            def response = qasePost("/result/${QASE_PROJECT_CODE}/${runId}", resultBody)
            if (response?.status == true || response?.result) {
                println "[QaseReporter] ✓ Reported to Qase: case #${caseId} = ${qaseStatus}"
            } else {
                println "[QaseReporter] WARNING: Unexpected response: ${response}"
            }
        } catch (Exception e) {
            println "[QaseReporter] ERROR reporting result: ${e.message}"
        }

        // If running single test case (not suite), complete the run after each test
        if (!suiteMode) {
            completeQaseRun()
        }
    }

    @AfterTestSuite
    def afterTestSuite(TestSuiteContext context) {
        completeQaseRun()
        suiteMode = false
    }

    static void createQaseRun() {
        def timestamp = new Date().format("yyyy-MM-dd HH:mm")
        def body = JsonOutput.toJson([
            title      : "Katalon Black Box Test - ${timestamp}",
            is_autotest: true
        ])
        
        try {
            def response = qasePost("/run/${QASE_PROJECT_CODE}", body)
            if (response?.result?.id) {
                runId = response.result.id
                println "[QaseReporter] Created Qase run #${runId}"
            } else {
                println "[QaseReporter] WARNING: Failed to create Qase run. Response: ${response}"
            }
        } catch (Exception e) {
            println "[QaseReporter] ERROR creating Qase run: ${e.message}"
        }
    }

    static void completeQaseRun() {
        if (runId > 0) {
            println "[QaseReporter] Completing Qase run #${runId}..."
            try {
                qasePost("/run/${QASE_PROJECT_CODE}/${runId}/complete", '{}')
                println "[QaseReporter] ✓ Qase run #${runId} completed"
            } catch (Exception e) {
                println "[QaseReporter] ERROR completing run: ${e.message}"
            }
        }
        runId = 0
    }

    static String mapStatus(String katalonStatus) {
        switch (katalonStatus) {
            case 'PASSED':  return 'passed'
            case 'FAILED':  return 'failed'
            case 'ERROR':   return 'blocked'
            default:        return 'skipped'
        }
    }

    static Object qasePost(String endpoint, String body) {
        def url = new URL("${QASE_API_BASE}${endpoint}")
        def conn = (HttpURLConnection) url.openConnection()
        conn.setRequestMethod('POST')
        conn.setRequestProperty('Content-Type', 'application/json')
        conn.setRequestProperty('Token', QASE_API_TOKEN)
        conn.setDoOutput(true)
        conn.setConnectTimeout(10000)
        conn.setReadTimeout(10000)
        
        conn.getOutputStream().write(body.getBytes('UTF-8'))
        
        def responseCode = conn.getResponseCode()
        def responseBody = ''
        
        try {
            responseBody = conn.getInputStream().getText('UTF-8')
        } catch (Exception e) {
            responseBody = conn.getErrorStream()?.getText('UTF-8') ?: ''
        }
        
        println "[QaseReporter] API ${endpoint} → ${responseCode}: ${responseBody.take(200)}"
        
        return new JsonSlurper().parseText(responseBody ?: '{}')
    }
}
