import {useNavigate} from 'react-router-dom';

function StudentItem({student, handleDeleteStudent})
{
  const navigate = useNavigate();
  const onEditClickHandler = () => 
  {
    navigate(`/editstudent/${student.id}`);
  };

  const onDeleteClickHandler = () =>
  {
    handleDeleteStudent(student.id);
  }

  return (
                  <tr>
                    <td>
                      <span onClick={onEditClickHandler}>
                        <i className="fa-solid fa-pencil edit"></i>
                      </span>
                    </td>
                    <td>{student.id}</td>
                    <td>{student.firstName}</td>
                    <td>{student.lastName}</td>
                    <td>{student.email}</td>
                    <td>{student.phone}</td>
                    <td>{student.address}</td>
                    <td>
                      <span onClick={onDeleteClickHandler}>
                        <i className="fa-solid fa-trash-can delete"></i>
                      </span>
                    </td>
                  </tr>
  )
}
export default StudentItem;