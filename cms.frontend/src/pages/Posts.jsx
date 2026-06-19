import { useEffect, useState } from "react";

import Header from "../components/Header";
import Footer from "../components/Footer";

import PostCard from "../components/posts/PostCard";

import { getPosts } from "../api/postApi";

function Posts() {

    const [posts, setPosts] = useState([]);

    useEffect(() => {
        loadPosts();
    }, []);

    const loadPosts = async () => {
        const data = await getPosts();
        setPosts(data);
    };

    return (
        <>
            <Header />

            <div className="max-w-7xl mx-auto py-10">

                <h1 className="text-4xl font-bold mb-8">
                    Tin tức & Bài viết
                </h1>

                <div className="
                    grid
                    grid-cols-1
                    md:grid-cols-2
                    lg:grid-cols-3
                    gap-8
                ">
                    {
                        posts.map(post => (
                            <PostCard
                                key={post.id}
                                post={post}
                            />
                        ))
                    }
                </div>

            </div>

            <Footer />
        </>
    );
}

export default Posts;