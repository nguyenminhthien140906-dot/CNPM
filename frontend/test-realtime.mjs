
import * as signalR from "@microsoft/signalr";

const API = "http://localhost:5000";
const TEAM_ID = 1;

async function login(username) {
  const response = await fetch(`${API}/api/Auth/login`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({
      usernameOrEmail: username,
      password: process.env.TEST_PASSWORD,
    }),
  });

  if (!response.ok) {
    throw new Error(`Đăng nhập ${username} thất bại: ${response.status}`);
  }

  const result = await response.json();
  return result.data.accessToken;
}

function createConnection(token) {
  return new signalR.HubConnectionBuilder()
    .withUrl(`${API}/hubs/chat`, {
      accessTokenFactory: () => token,
    })
    .configureLogging(signalR.LogLevel.Warning)
    .build();
}

let student1;
let student2;

try {
  if (!process.env.TEST_PASSWORD) {
    throw new Error("Chưa đặt TEST_PASSWORD");
  }

  const token1 = await login("student01");
  const token2 = await login("student02");

  console.log("OK: Đăng nhập 2 tài khoản");

  student1 = createConnection(token1);
  student2 = createConnection(token2);
  student1.on("ReceiveMessage", () => {
  console.log("OK: Student01 cũng nhận được tin nhắn");
});

  const received = new Promise((resolve, reject) => {
    const timeout = setTimeout(
      () => reject(new Error("Student02 không nhận được tin nhắn")),
      10000
    );

    student2.on("ReceiveMessage", (message) => {
      clearTimeout(timeout);
      resolve(message);
    });
  });

  await student1.start();
  await student2.start();

  console.log("OK: Hai kết nối SignalR hoạt động");

  await student1.invoke("JoinTeam", TEAM_ID);
  await student2.invoke("JoinTeam", TEAM_ID);

  console.log("OK: Cả hai đã vào nhóm", TEAM_ID);

  const content = `Realtime test ${Date.now()}`;

  await student1.invoke("SendMessage", TEAM_ID, content);

  const message = await received;

  if (message.content !== content) {
    throw new Error("Nội dung tin nhắn không khớp");
  }

  console.log("THÀNH CÔNG: Student02 nhận được tin nhắn!");
  console.log({
    messageId: message.messageId,
    teamId: message.teamId,
    content: message.content,
  });
} catch (error) {
  console.error("TEST THẤT BẠI:", error.message);
  process.exitCode = 1;
} finally {
  if (student1) await student1.stop();
  if (student2) await student2.stop();
}