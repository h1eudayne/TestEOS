const { remote } = require("webdriverio")

async function test(){

const driver = await remote({
hostname: "127.0.0.1",
port: 4723,
path: "/",
capabilities: {
platformName: "Windows",
deviceName: "WindowsPC",
app: "C:\\Desktop\\TestEOS\\EOSClient\\bin\\Debug\\EOSClient.exe"
}
})

console.log("EOS App started")

await driver.pause(5000)

await driver.deleteSession()

}

test()