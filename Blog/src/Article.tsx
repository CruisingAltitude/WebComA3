import React from 'react';
import { useState, useEffect } from 'react'
import { useParams } from 'react-router';
import '../styles/Blog.css'
import Markdown from 'react-markdown'

function Article(){
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
    <section>
      { data.map((item) =>
        <article>
          <h1>{ item['articleTitle'] }</h1>
          <details className='author'>
            <summary>{ item['author']['username'] } { item['publishTimeUTC'] }</summary>
          </details>
          <Markdown className="article-body">{ item['articleBody'] }</Markdown>
        </article>
      )}
    </section>
  )
}

export default Article