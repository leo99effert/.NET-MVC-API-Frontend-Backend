import {useNavigate} from 'react-router-dom';

function CourseItem({course, handleDeleteCourse})
{
  const navigate = useNavigate();
  const onEditClickHandler = () => 
  {
    navigate(`/editcourse/${course.id}`);
  };

  const onDeleteClickHandler = () =>
  {
    handleDeleteCourse(course.id);
  }

  return (
                  <tr>
                    <td>
                      <span onClick={onEditClickHandler}>
                        <i className="fa-solid fa-pencil edit"></i>
                      </span>
                    </td>
                    <td>{course.id}</td>
                    <td>{course.title}</td>
                    <td>{course.length}</td>
                    <td>{course.category}</td>
                    <td>{course.description}</td>
                    <td>{course.details}</td>
                    <td>
                      <span onClick={onDeleteClickHandler}>
                        <i className="fa-solid fa-trash-can delete"></i>
                      </span>
                    </td>
                  </tr>
  )
}
export default CourseItem;