
import * as signalR from "@microsoft/signalr";

const API = "http://localhost:5000";

let connection;

try {
  if (!process.env.TEST_PASSWORD) {
    throw new Error("Chua co TEST_PASSWORD");
  }

  // Dang nhap student02
  const response = await fetch(`${API}/api/Auth/login`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({
      usernameOrEmail: "student02",
      password: process.env.TEST_PASSWORD,
    }),
  });

  if (!response.ok) {
    throw new Error(`Dang nhap that bai: ${response.status}`);
  }

  const result = await response.json();
  const token = result.data.accessToken;

  console.log("OK: Dang nhap student02 thanh cong");

  // Ket noi NotificationHub
  connection = new signalR.HubConnectionBuilder()
    .withUrl(`${API}/hubs/notifications`, {
      accessTokenFactory: () => token,
    })
    .configureLogging(signalR.LogLevel.Warning)
    .build();

  // Lang nghe thong bao
  connection.on("ReceiveNotification", (notification) => {
    console.log("DA NHAN THONG BAO REALTIME:");
    console.log(JSON.stringify(notification, null, 2));
  });

  await connection.start();

  console.log("OK: Ket noi NotificationHub thanh cong");
  console.log("Dang cho thong bao... Nhan Ctrl+C de dung.");

  // Giu ket noi de nhan thong bao
  setInterval(() => {}, 1000);

} catch (error) {
  console.error("LOI:", error.message);

  if (connection) {
    await connection.stop();
  }

  process.exitCode = 1;
}

