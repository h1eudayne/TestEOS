const { remote } = require("webdriverio")

async function loginTest(){

const driver = await remote({
hostname:"127.0.0.1",
port:4723,
path:"/",
capabilities:{
platformName:"Windows",
deviceName:"WindowsPC",
app:"C:\\Desktop\\TestEOS\\EOSClient\\bin\\Debug\\EOSClient.exe",
appWorkingDir:"C:\\Desktop\\TestEOS\\EOSClient\\bin\\Debug"
}
})

console.log("App started")

await driver.pause(5000)


// =====================
// 1 CLICK CHECKBOX
// =====================

const checkbox = await driver.$('accessibility id:chbRead')

await checkbox.waitForDisplayed({timeout:5000})

// focus vào checkbox
await checkbox.click()

// tick bằng keyboard
await driver.keys("Space")

console.log("Checkbox checked")


// =====================
// 2 WAIT NEXT ENABLE
// =====================

const nextBtn = await driver.$('name:Next')

await driver.waitUntil(
async () => await nextBtn.isEnabled(),
{
timeout:10000,
timeoutMsg:"Next button not enabled"
}
)

console.log("Next button enabled")


// =====================
// 3 CLICK NEXT
// =====================

await nextBtn.click()

console.log("Next clicked")

await driver.pause(4000)


// =====================
// LOGIN
// =====================

const examCode = await driver.$('accessibility id:txtExamCode')
await examCode.setValue("123456")

const username = await driver.$('accessibility id:txtUser')
await username.setValue("student")

const password = await driver.$('accessibility id:txtPassword')
await password.setValue("123")

console.log("Login info entered")

await driver.pause(500)

const loginBtn = await driver.$('accessibility id:btnLogin')
await loginBtn.click()

console.log("Login submitted")

await driver.pause(5000)

await driver.saveScreenshot("./screenshots/login_result.png")

await driver.deleteSession()

}

loginTest()