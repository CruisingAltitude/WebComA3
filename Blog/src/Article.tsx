import React from 'react';
import { useState, useEffect } from 'react'
import { useParams } from 'react-router';
import '../styles/Blog.css'

function Article(props){
  const params = useParams();
  const url = "http://localhost:5177/api/article/" + params.id;
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
      { data.map((item) =>
        <div>
          <p>{ item['articleTitle'] }</p>
          <p>{ item['author']['username'] }</p>
          <p>{ item['status'] }</p>
          <p>{ item['creationTimeUTC'] }</p>
          <p>{ item['publishTimeUTC'] }</p>
          <p>{ item['articleSummary'] }</p>
          <p>{ item['articleBody'] }</p>
        </div>
      )}
    </>
  )
}

export default Article