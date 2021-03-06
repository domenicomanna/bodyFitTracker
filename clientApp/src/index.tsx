import React from 'react';
import ReactDOM from 'react-dom';
import * as serviceWorker from './serviceWorker';
import { Router } from 'react-router-dom';
import  history  from './utils/history';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import './global.css';
import './overrides/reactToastifyOverride.css';
import App from './App';
import UserContextProvider from './contexts/UserContext';
import './utils/fontawesomeLibrary';

const app = (
  <UserContextProvider>
    <Router history={history}>
      <App />
      <ToastContainer position='bottom-right' />
    </Router>
  </UserContextProvider>
);

ReactDOM.render(app, document.getElementById('root'));

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
