import React from "react";
import ReactDOM from "react-dom";
import ReactDOMServer from "react-dom/server";

import App from "./app";

global.React = React;
global.ReactDOM = ReactDOM;
global.ReactDOMServer = ReactDOMServer;

global.App = App;
