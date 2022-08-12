import logo from './logo.svg';
import Layout from './components/Layout';
import './App.css';
import './dove-theme/styles.css';

function App() {
  return (
    <div className="App">
      <Layout />

      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        
        This is watching the files!

      </header>
    </div>
  );
}

export default App;
