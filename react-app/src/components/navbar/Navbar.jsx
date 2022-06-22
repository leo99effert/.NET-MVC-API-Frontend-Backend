import {NavLink} from 'react-router-dom';
function Navbar()
{
  return (
    <nav id="navbar">
      <h1 className="logo">
        <span className="text-primary"><i className="fa-solid fa-book-open"></i> Westcoast</span>
        Education
      </h1>
      <ul>
        <li>
          <NavLink to='/'>Home</NavLink>

          <NavLink to='/listcourse'>Courses</NavLink>
          <NavLink to='/addcourse'>Add Course</NavLink>

          <NavLink to='/liststudent'>Students</NavLink>
          <NavLink to='/addstudent'>Add Student</NavLink>

          <NavLink to='/listteacher'>Teachers</NavLink>
          <NavLink to='/addteacher'>Add Teacher</NavLink>
        </li>
      </ul>
    </nav>
  );
}
export default Navbar;