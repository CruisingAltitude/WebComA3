import { Link } from 'react-router-dom'
import '../styles/Nav.css'

function Nav() {
  return (
    <nav>
     <ul>
        <li><Link to="/">Blog</Link></li>
        <li><Link to="aboutme">About Me</Link></li>
        <li><Link to="contact">Contact</Link></li>
      </ul> 
    </nav>
  )
}

export default Nav
