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

// ★ Dọn dẹp process cũ từ lần test trước
'taskkill /F /IM EOSClient.exe /T'.execute()

Thread.sleep(2000)

// Step 1: Mở app EOS Client
Windows.startApplication('C:\\Desktop\\TestEOS\\EOSClient\\bin\\Debug\\EOSClient.exe')

Windows.delay(3)

Windows.switchToWindowTitle('Nội quy kỳ thi')

Windows.click(findWindowsObject('Object Repository/CheckBox(1)'))

Windows.click(findWindowsObject('Object Repository/Button(2)'))

// ★ QUAN TRỌNG: Chuyển sang cửa sổ Login Form sau khi click Next
// Dialog "Nội quy kỳ thi" đóng lại, Login Form mở ra là window mới
Windows.delay(3)

Windows.switchToWindowTitle('EOS Login Form')

Windows.click(findWindowsObject('Object Repository/Edit'))

Windows.clearText(findWindowsObject('Object Repository/Edit'))

Windows.sendKeys(findWindowsObject('Object Repository/Edit'), 'MATH001')

Windows.click(findWindowsObject('Object Repository/Edit(1)'))

Windows.clearText(findWindowsObject('Object Repository/Edit(1)'))

Windows.sendKeys(findWindowsObject('Object Repository/Edit(1)'), 'student1')

Windows.click(findWindowsObject('Object Repository/Edit(2)'))

Windows.clearText(findWindowsObject('Object Repository/Edit(2)'))

Windows.sendKeys(findWindowsObject('Object Repository/Edit(2)'), 'pass123')

Windows.click(findWindowsObject('Object Repository/Button(3)'))

// Dùng keyboard để chọn server thay vì click ListItem popup
// Gõ tên server và Enter, hoặc dùng phím Down để chọn
Windows.delay(1)

Windows.sendKeys(findWindowsObject('Object Repository/Button(3)'), Keys.chord(Keys.DOWN, Keys.ENTER))

// Windows.click(findWindowsObject('Object Repository/Button(4)'))
// ★ Chuyển sang cửa sổ EOS Exam Client sau khi login thành công
Windows.delay(5)

Windows.switchToWindowTitle('EOS Exam Client')

Windows.click(findWindowsObject('Object Repository/Button(5)'))

Windows.click(findWindowsObject('Object Repository/CheckBox(2)'))

Windows.click(findWindowsObject('Object Repository/Button(6)'))

Windows.click(findWindowsObject('Object Repository/CheckBox(2)'))

Windows.click(findWindowsObject('Object Repository/Button(6)'))

Windows.click(findWindowsObject('Object Repository/CheckBox(2)'))

Windows.click(findWindowsObject('Object Repository/Button(6)'))

Windows.click(findWindowsObject('Object Repository/CheckBox(3)'))

Windows.click(findWindowsObject('Object Repository/Button(6)'))

Windows.click(findWindowsObject('Object Repository/CheckBox(4)'))

Windows.click(findWindowsObject('Object Repository/CheckBox(5)'))

Windows.click(findWindowsObject('Object Repository/Button(7)'))

Windows.click(findWindowsObject('Object Repository/Button(8)'))

Windows.click(findWindowsObject('Object Repository/Button(9)'))

Windows.click(findWindowsObject('Object Repository/Button(10)'))

