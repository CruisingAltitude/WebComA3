import { useState, useEffect } from 'react'
import '../styles/Blog.css'

function Blog(){
  const url = "http://localhost:5177/api/article";
  const [data, setData] = useState([]);

  function getData(){
    fetch(url)
      .then(function(response){
        return response.json();
      })
      .then(function(myJson) {
        setData(myJson)
      });
  }

  useEffect(()=>{
    getData()
  },[])

  return (
    <>
      <h1>Blog</h1>
      <div className="card-gallery">
      {
        // data.map((item) => <li key={item['articleId']}>{item['articleTitle']} {item['creationTimeUTC']}</li>)
        data.map((item) => 
        <div className="card">
          <div className="card-body">
            <h5 className="card-title">{item['articleTitle']}</h5>
            <p className="card-text">{item['articleSummary']}</p>
          </div>
        </div>)
      }
      </div>
     </>
  )
}

export default Blog