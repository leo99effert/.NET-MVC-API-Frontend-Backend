import {useNavigate} from 'react-router-dom';

function TeacherItem({teacher, handleDeleteTeacher})
{
  const navigate = useNavigate();
  const onEditClickHandler = () => 
  {
    navigate(`/editteacher/${teacher.id}`);
  };

  const onDeleteClickHandler = () =>
  {
    handleDeleteTeacher(teacher.id);
  }

  return (
            <tr>
            <td>
              <span onClick={onEditClickHandler}>
                <i className="fa-solid fa-pencil edit"></i>
              </span>
            </td>
            <td>{teacher.id}</td>
            <td>{teacher.firstName}</td>
            <td>{teacher.lastName}</td>
            <td>{teacher.email}</td>
            <td>{teacher.phone}</td>
            <td>{teacher.address}</td>
            <td>
              <span onClick={onDeleteClickHandler}>
                <i className="fa-solid fa-trash-can delete"></i>
              </span>
            </td>
          </tr>
  )
}
export default TeacherItem;