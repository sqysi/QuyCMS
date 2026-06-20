import { Link } from "react-router-dom";

function LatestPosts({ posts }) {
  return (
    <section className="bg-white py-20">
      <div className="max-w-7xl mx-auto px-6">
        <h2
          className="
            text-4xl
            font-bold
            text-center
            mb-12
        "
        >
          Xu hướng thời trang
        </h2>

        <div
          className="
            grid
            md:grid-cols-3
            gap-8
        "
        >
          {posts.map((post) => (
            <div
              key={post.id}
              className="
                bg-white
                rounded-xl
                overflow-hidden
                shadow-md
              "
            >
              <img
                src={`https://localhost:7108${post.imageUrl}`}
                alt={post.title}
                className="
                  w-full
                  h-56
                  object-cover
                "
              />

              <div className="p-5">
                <h3
                  className="
                    font-bold
                    text-lg
                    mb-3
                "
                >
                  {post.title}
                </h3>

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
          ))}
        </div>
      </div>
    </section>
  );
}

export default LatestPosts;
