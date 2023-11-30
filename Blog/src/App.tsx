import { Route, Routes } from 'react-router-dom'
import '../styles/App.css'
import Nav from './Nav'
import Blog from './Blog'
import AboutMe from './AboutMe'
import Contact from './Contact'

function App() {
  return (
    <>
      <Nav />
      <div className="route">
        <Routes>
          <Route path="/" element={<Blog />} />
          <Route path="aboutme" element={<AboutMe />} />
          <Route path="contact" element={<Contact />} />
        </Routes>
      </div>
    </>
  )
}

export default App
