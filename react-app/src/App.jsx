import {BrowserRouter as Router, Route, Routes} from 'react-router-dom';
import Navbar from './components/navbar/Navbar';
import Home from './components/home/Home';
import CourseList from "./components/Courses/CourseList";
import AddCourse from './components/Courses/AddCourse';
import EditCourse from './components/Courses/EditCourse';
import './utilities.css';
import './styles.css';

import StudentList from "./components/Students/StudentList";
import AddStudent from './components/Students/AddStudent';
import EditStudent from './components/Students/EditStudent';
import TeacherList from "./components/Teachers/TeacherList";
import AddTeacher from './components/Teachers/AddTeacher';
import EditTeacher from './components/Teachers/EditTeacher';



function App()
{
  return (
            <Router>
              <Navbar/>
              <main>
                <Routes>
                  <Route path='/' element={<Home/>}/>

                  <Route path='/listcourse' element={<CourseList/>}/>
                  <Route path='/addcourse' element={<AddCourse/>}/>
                  <Route path='/editcourse/:id' element={<EditCourse/>}/>

                  <Route path='/liststudent' element={<StudentList/>}/>
                  <Route path='/addstudent' element={<AddStudent/>}/>
                  <Route path='/editstudent/:id' element={<EditStudent/>}/>

                  <Route path='/listteacher' element={<TeacherList/>}/>
                  <Route path='/addteacher' element={<AddTeacher/>}/>
                  <Route path='/editteacher/:id' element={<EditTeacher/>}/>
                </Routes>
              </main>
            </Router>
  )
}
export default App;