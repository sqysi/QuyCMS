import { Link } from "react-router-dom";

function PostCard({ post }) {
  return (
    <div
      className="
            bg-white
            rounded-xl
            shadow-md
            overflow-hidden
        "
    >
      <img
        src={`https://localhost:7108${post.imageUrl}`}
        alt={post.title}
        className="
                    h-60
                    w-full
                    object-cover
                "
      />

      <div className="p-4">
        <h3
          className="
                    text-xl
                    font-bold
                "
        >
          {post.title}
        </h3>

        <p
          className="
                    text-gray-600
                    mt-2
                "
        >
          {post.summary}
        </p>
        <Link
          to={`/posts/${post.id}`}
          className="
    inline-block
    mt-4
    text-emerald-500
    font-semibold
  "
        >
          Đọc thêm →
        </Link>
      </div>
    </div>
  );
}

export default PostCard;
