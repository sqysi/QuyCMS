import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

import Header from "../components/Header";
import Footer from "../components/Footer";

import { getPostById } from "../api/postApi";

function PostDetail() {

  const { id } = useParams();

  const [post, setPost] = useState(null);

  useEffect(() => {
    loadPost();
  }, [id]);

  const loadPost = async () => {
    const data = await getPostById(id);
    setPost(data);
  };

  if (!post) {
    return (
      <div className="text-center py-20">
        Đang tải...
      </div>
    );
  }

  return (
    <>
      <Header />

      <div
        className="
          max-w-4xl
          mx-auto
          py-12
          px-6
        "
      >
        <img
          src={`https://localhost:7108${post.imageUrl}`}
          alt={post.title}
          className="
            w-full
            h-[500px]
            object-cover
            rounded-xl
            mb-8
          "
        />

        <h1
          className="
            text-5xl
            font-bold
            mb-4
          "
        >
          {post.title}
        </h1>

        <p
          className="
            text-gray-500
            mb-8
          "
        >
          {new Date(
            post.createdDate
          ).toLocaleDateString("vi-VN")}
        </p>

        <div
          className="
            text-lg
            leading-9
            text-gray-700
          "
        >
          {post.content}
        </div>
      </div>

      <Footer />
    </>
  );
}

export default PostDetail;