import {useEffect, useState} from 'react';
import CourseItem from "./CourseItem";

function CourseList()
{
  const [courses, setCourses] = useState([]);

  useEffect(() =>
  {
    loadCourses();
  },[]);

  const loadCourses = async() =>
  {
    const url = `${process.env.REACT_APP_BASEURL}/courses`;
    const response = await fetch(url);

    if (!response.ok) 
    {
      console.log("Error");
    }

    setCourses(await response.json());
  }

  const deleteCourse = async(id) =>
  {
    console.log(`Deleting course with id ${id}`);
    const url = `${process.env.REACT_APP_BASEURL}/courses/${id}`;
    const response = await fetch(url, 
      {
        method: 'DELETE'
      });
      console.log(response);
    if (response.status >= 200 && response.status <= 299) 
    {
      console.log('Course is deleted');
      loadCourses();
    } 
    else 
      console.log('Error');
  }
  return (
              <table>
                <thead>
                  <tr>
                    <th></th>
                    <th>Id</th>
                    <th>Title</th>
                    <th>Length</th>
                    <th>Category</th>
                    <th>Description</th>
                    <th>Details</th>
                    <th></th>
                  </tr>
                </thead>
                <tbody>
                  {courses.map((course) => 
                  (
                    <CourseItem 
                    course={course} 
                    key={course.id}
                    handleDeleteCourse={deleteCourse}/>
                  ))}
                </tbody>
              </table>
  )
}
export default CourseList;
