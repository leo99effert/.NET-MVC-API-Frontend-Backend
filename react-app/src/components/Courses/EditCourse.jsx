import {useState, useEffect} from 'react';
import {useParams} from 'react-router-dom';

function EditCourse()
{
  const params = useParams();

  const [id, setId] = useState('');
  const [title, setTitle] = useState('');
  const [length, setLength] = useState('');
  const [category, setCategory] = useState('');
  const [description, setDescription] = useState('');
  const [details, setDetails] = useState('');

  useEffect(() =>
  {
    fetchCourse(params.id);
  },[params.id]);

  const fetchCourse = async (id) =>
  {
    const url = `${process.env.REACT_APP_BASEURL}/courses/${id}`;
    const response = await fetch(url);

    if (!response.ok) 
    {
      console.log("Error");
    }
    const course = await response.json();
    console.log(course);

    setId(course.id);
    setTitle(course.title);
    setLength(course.length);
    setCategory(course.category);
    setDescription(course.description);
    setDetails(course.details);

  };
  const onHandleIdTextChanged = (e) =>
  {
    setId(e.target.value);
  };
  const onHandleTitleTextChanged = (e) =>
  {
    setTitle(e.target.value);
  };
  const onHandleLengthTextChanged = (e) =>
  {
    setLength(e.target.value);
  };
  const onHandleCategoryTextChanged = (e) =>
  {
    setCategory(e.target.value);
  };
  const onHandleDescriptionTextChanged = (e) =>
  {
    setDescription(e.target.value);
  };
  const onHandleDetailsTextChanged = (e) =>
  {
    setDetails(e.target.value);
  };
  const handleSaveCourse = (e) =>
  {
    e.preventDefault();
    const course = 
    {
      title,
      length,
      category,
      description,
      details
    };
    console.log(course);
    saveCourse(course);
  };
  const saveCourse = async (course) =>
  {
    const url = `${process.env.REACT_APP_BASEURL}/courses/${id}`;
    const response = await fetch(url, 
      {
        method: 'PUT',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(course)
      });
      console.log(response);
    if (response.status >= 200 && response.status <= 299) 
    {
      console.log("Course is saved");
    }     
  };
  return (
    <>
      <h1 className='page-title'>Edit Course</h1>
      <section className='form-container'>
        <h4>Course Info</h4>
        <section className='form-wrapper'>
          <form className='form' onSubmit={handleSaveCourse}>
          <input
                onChange={onHandleIdTextChanged}
                value={id}
                type='hidden'
                id='id'
                name='id'
              />
            <div className='form-control'>
              <label htmlFor='title'>Title</label>
              <input
                onChange={onHandleTitleTextChanged}
                value={title}
                type='text'
                id='title'
                name='title'
              />
            </div>
            <div className='form-control'>
              <label htmlFor='length'>Length</label>
              <input
                onChange={onHandleLengthTextChanged}
                value={length}
                type='text'
                id='length'
                name='length'
              />
            </div>
            <div className='form-control'>
              <label htmlFor='category'>Category</label>
              <input
                onChange={onHandleCategoryTextChanged}
                value={category}
                type='text'
                id='category'
                name='category'
              />
            </div>
            <div className='form-control'>
              <label htmlFor='description'>Description</label>
              <input
                onChange={onHandleDescriptionTextChanged}
                value={description}
                type='text'
                id='description'
                name='description'
              />
            </div>
            <div className='form-control'>
              <label htmlFor='details'>Details</label>
              <input
                onChange={onHandleDetailsTextChanged}
                value={details}
                type='text'
                id='details'
                name='details'
              />
            </div>
            <div className='buttons'>
              <button type='submit' className='btn'>
                Save
              </button>
            </div>
          </form>
        </section>
      </section>
    </>
  );
}
export default EditCourse