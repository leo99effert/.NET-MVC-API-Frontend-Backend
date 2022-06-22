import {useEffect, useState} from 'react';
import StudentItem from "./StudentItem";

function StudentList()
{
  const [students, setStudents] = useState([]);

  useEffect(() =>
  {
    loadStudents();
  },[]);

  const loadStudents = async() =>
  {
    const url = `${process.env.REACT_APP_BASEURL}/students`;
    const response = await fetch(url);

    if (!response.ok) 
    {
      console.log("Error");
    }

    setStudents(await response.json());
  }

  const deleteStudent = async(id) =>
  {
    console.log(`Deleting student with id ${id}`);
    const url = `${process.env.REACT_APP_BASEURL}/students/${id}`;
    const response = await fetch(url, 
      {
        method: 'DELETE'
      });
      console.log(response);
    if (response.status >= 200 && response.status <= 299) 
    {
      console.log('Student is deleted');
      loadStudents();
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
                  {students.map((student) => 
                  (
                    <StudentItem 
                    student={student} 
                    key={student.id}
                    handleDeleteStudent={deleteStudent}/>
                  ))}
                </tbody>
              </table>
  )
}
export default StudentList;
