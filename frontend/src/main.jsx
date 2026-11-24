import React from "react";
import ReactDOM from "react-dom/client";
import { BrowserRouter } from "react-router-dom";

import App from "./App";
import { AuthProvider } from "./contexts/AuthContext";
import { DemoProvider } from "./contexts/DemoContext";
import { NotificationProvider } from "./contexts/NotificationContext";

import "./assets/styles/global.css";
import "./assets/styles/variables.css";
import "./assets/styles/components.css";
import "./assets/styles/responsive.css";
import "./assets/styles/app.css";

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <BrowserRouter>
      <DemoProvider>
        <AuthProvider>
          <NotificationProvider>
            <App />
          </NotificationProvider>
        </AuthProvider>
      </DemoProvider>
    </BrowserRouter>
  </React.StrictMode>
);
