import React from 'react';
import { connect } from 'react-redux';
import SharedComponent from 'hvb-shared-frontend/src/components/SharedComponent';

//<SharedComponent color={'pink'} />

const Home = props => (
    <div>
        <h3>Welcome back, </h3>
        <hr className="style14" />
        <p>Welcome to Artnivora Studio, you can use this as a start for your own project</p>
        <ul>
            <li><a href='https://get.asp.net/'>ASP.NET Core</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform server-side code</li>
            <li><a href='https://facebook.github.io/react/'>React</a> and <a href='https://redux.js.org/'>Redux</a> for client-side code</li>
            <li><a href='http://getbootstrap.com/'>Bootstrap</a> for layout and styling</li>
        </ul>
        
        <p>To help you get started, we've also set up:</p>
        <ul>
            <li><strong>Client-side navigation</strong></li>
            <li><strong>Development server integration</strong>. In development mode, the development server from <code>create-react-app</code> runs in the background automatically, so your client-side resources are dynamically built on demand and the page refreshes when you modify any file.</li>
            <li><strong>Efficient production builds</strong>. In production mode, development-time features are disabled, and your <code>dotnet publish</code> configuration produces minified, efficiently bundled JavaScript files.</li>
        </ul>
        <p>The <code>App</code> subdirectory is a standard React application based on the <code>create-react-app</code> template. If you open a command prompt in that directory, you can run <code>npm</code> commands such as <code>npm test</code> or <code>npm install</code>.</p>
    </div>
);

export default connect()(Home);
