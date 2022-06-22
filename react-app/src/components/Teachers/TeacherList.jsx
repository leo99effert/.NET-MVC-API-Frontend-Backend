import {useEffect, useState} from 'react';
import TeacherItem from "./TeacherItem";

function TeacherList()
{
  const [teachers, setTeachers] = useState([]);

  useEffect(() =>
  {
    loadTeachers();
  },[]);

  const loadTeachers = async() =>
  {
    const url = `${process.env.REACT_APP_BASEURL}/teachers`;
    const response = await fetch(url);

    if (!response.ok) 
    {
      console.log("Error");
    }

    setTeachers(await response.json());
  }

  const deleteTeacher = async(id) =>
  {
    console.log(`Deleting teacher with id ${id}`);
    const url = `${process.env.REACT_APP_BASEURL}/teachers/${id}`;
    const response = await fetch(url, 
      {
        method: 'DELETE'
      });
      console.log(response);
    if (response.status >= 200 && response.status <= 299) 
    {
      console.log('Teacher is deleted');
      loadTeachers();
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
                    <th>First Name</th>
                    <th>Last Name</th>
                    <th>Email</th>
                    <th>Phone</th>
                    <th>Address</th>
                    <th></th>
                  </tr>
                </thead>
                <tbody>
                  {teachers.map((teacher) => 
                  (
                    <TeacherItem 
                    teacher={teacher} 
                    key={teacher.id}
                    handleDeleteTeacher={deleteTeacher}/>
                  ))}
                </tbody>
              </table>
  )
}
export default TeacherList;
