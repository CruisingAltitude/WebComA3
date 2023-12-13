import { Route, Routes } from 'react-router-dom'
import '../styles/App.css'
import Nav from './Nav'
import Blog from './Blog'
import AboutMe from './AboutMe'
import Contact from './Contact'
import Article from './Article'
import Footer from './Footer'

function App() {
  return (
    <>
      <Nav />
      <div className="route">
        <Routes>
          <Route path="/" element={<Blog />} />
          <Route path="aboutme" element={<AboutMe />} />
          <Route path="contact" element={<Contact />} />
          <Route path="article/:id" element={<Article />} />
        </Routes>
      </div>
      <Footer />
    </>
  )
}

export default App
