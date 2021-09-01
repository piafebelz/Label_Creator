import React from "react";
// import logo from "./logo.svg";
import "./App.css";
import BarcodeGen from "./components/BarcodeGen";
import { ThemeProvider } from "@oplog/express";
import theme from "./theme";

export default function App() {
  return (
    <ThemeProvider customTheme={theme}>
      <BarcodeGen />
    </ThemeProvider>
  );
}
