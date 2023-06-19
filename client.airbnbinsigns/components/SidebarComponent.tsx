import React from 'react';

function Sidebar() {
  return (
    <div className="sidebar">
      <h2>Statistics</h2>
      <ul>
        <li>Number of markers: 2</li>
        <li>Zoom level: 12</li>
      </ul>

      <style jsx>{`
        .sidebar {
          flex: 0 0 auto;
          width: 300px;
          background-color: #f5f5f5;
          padding: 32px;
          border-radius: 4px;
        }

        ul {
          list-style: none;
          margin: 0;
          padding: 0;
        }

        li {
          margin-bottom: 16px;
        }
      `}</style>
    </div>
  );
}

export default Sidebar;
