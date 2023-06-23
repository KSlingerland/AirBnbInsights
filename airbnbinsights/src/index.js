import React, { Children } from 'react';
import ReactDOM from 'react-dom/client';

import './index.css';
import reportWebVitals from './reportWebVitals';
import { RouterProvider, createBrowserRouter } from 'react-router-dom';

import Listing from './routes/Listing';
import Root from './routes/root';

const router = createBrowserRouter([
  {
    path: '/',
    element: <Root />,
    children:[
      {
        path: 'listing/:listingid',
        element: <Listing />
      },
    ],
  },
])

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <RouterProvider router={router}/>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();