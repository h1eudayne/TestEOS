import static com.kms.katalon.core.checkpoint.CheckpointFactory.findCheckpoint
import static com.kms.katalon.core.testcase.TestCaseFactory.findTestCase
import static com.kms.katalon.core.testdata.TestDataFactory.findTestData
import static com.kms.katalon.core.testobject.ObjectRepository.findTestObject
import static com.kms.katalon.core.testobject.ObjectRepository.findWindowsObject
import com.kms.katalon.core.checkpoint.Checkpoint as Checkpoint
import com.kms.katalon.core.cucumber.keyword.CucumberBuiltinKeywords as CucumberKW
import com.kms.katalon.core.mobile.keyword.MobileBuiltInKeywords as Mobile
import com.kms.katalon.core.model.FailureHandling as FailureHandling
import com.kms.katalon.core.testcase.TestCase as TestCase
import com.kms.katalon.core.testdata.TestData as TestData
import com.kms.katalon.core.testng.keyword.TestNGBuiltinKeywords as TestNGKW
import com.kms.katalon.core.testobject.TestObject as TestObject
import com.kms.katalon.core.webservice.keyword.WSBuiltInKeywords as WS
import com.kms.katalon.core.webui.keyword.WebUiBuiltInKeywords as WebUI
import com.kms.katalon.core.windows.keyword.WindowsBuiltinKeywords as Windows
import internal.GlobalVariable as GlobalVariable
import org.openqa.selenium.Keys as Keys

// ============================================
// EOS Client Login Test
// Flow: Exam Rules -> Login Form -> Login
// ============================================

// Step 1: Start EOS application - opens "Nội quy kỳ thi" (Exam Rules) dialog
Windows.startApplicationWithTitle('D:\\EOSClient_source\\EOSClient\\bin\\Debug\\EOSClient.exe', 'Nội quy kỳ thi')

// Wait for UI to fully load
Windows.delay(2)

// Step 2: Accept exam rules - tick the checkbox
Windows.click(findWindowsObject('Object Repository/chkAgreeRules'))

// Step 3: Click Next to proceed to Login Form
Windows.click(findWindowsObject('Object Repository/btnNext'))

// Step 4: Switch to the Login Form window
// The rules dialog closes and a new Login Form window opens
// We need to switch the driver session to the new window
Windows.delay(3)
Windows.switchToWindowTitle('EOS Login Form')

// Step 5: Input Exam Code
Windows.click(findWindowsObject('Object Repository/txtExamCode'))
Windows.setText(findWindowsObject('Object Repository/txtExamCode'), 'MATH001')

// Step 6: Input Username
Windows.click(findWindowsObject('Object Repository/txtUser'))
Windows.setText(findWindowsObject('Object Repository/txtUser'), 'student1')

// Step 7: Input Password
Windows.click(findWindowsObject('Object Repository/txtPassword'))
Windows.setText(findWindowsObject('Object Repository/txtPassword'), 'pass123')

// Step 8: Click Login
Windows.click(findWindowsObject('Object Repository/btnLogin'))

// Wait for login to process
Windows.delay(3)
