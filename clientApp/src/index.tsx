import React from 'react';
import ReactDOM from 'react-dom';
import './global.css';
import App from './App';
import * as serviceWorker from './serviceWorker';
import { BrowserRouter as Router } from 'react-router-dom';
import UserContextProvider from './contexts/UserContext';

const app = (
  <UserContextProvider>
    <Router>
      <App />
    </Router>
  </UserContextProvider>
);

ReactDOM.render(app, document.getElementById('root'));

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
