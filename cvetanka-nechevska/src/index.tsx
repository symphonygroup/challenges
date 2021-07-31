import React from "react";
import ReactDOM from "react-dom";

import Home from "./components/Home";

const App = () => {
  return <Home />;
};

ReactDOM.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
  document.getElementById("root")
);
