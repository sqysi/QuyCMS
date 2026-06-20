import { useEffect, useState } from "react";
<<<<<<< HEAD
import { getProducts } from "../api/productApi";
=======
import { Link } from "react-router-dom";
>>>>>>> 6deb3d7 (cap nhat them frontend)

import Header from "../components/Header";
import Banner from "../components/Banner";
import ProductCard from "../components/products/ProductCard";
import Footer from "../components/Footer";
<<<<<<< HEAD
import { Link } from "react-router-dom";

function Home() {
  const [products, setProducts] = useState([]);

  useEffect(() => {
    loadProducts();
=======
import CategoryMenu from "../components/categories/CategoryMenu";
import LatestPosts from "../components/posts/LatestPosts";

import {
  getProducts,
  getProductsByCategory,
} from "../api/productApi";

import { getPosts } from "../api/postApi";

function Home() {
  const [products, setProducts] = useState([]);
  const [posts, setPosts] = useState([]);

  useEffect(() => {
    loadProducts();
    loadPosts();
>>>>>>> 6deb3d7 (cap nhat them frontend)
  }, []);

  const loadProducts = async () => {
    const data = await getProducts();
    setProducts(data);
  };
<<<<<<< HEAD
  const featuredProducts = products.slice(0, 4);
=======

  const loadPosts = async () => {
    const data = await getPosts();
    setPosts(data.slice(0, 3));
  };

  const handleSelectCategory = async (categoryId) => {
    if (!categoryId) {
      loadProducts();
      return;
    }

    const data =
      await getProductsByCategory(categoryId);

    setProducts(data);
  };

  const featuredProducts = products.slice(0, 8);

>>>>>>> 6deb3d7 (cap nhat them frontend)
  return (
    <>
      <Header />

      <Banner />

<<<<<<< HEAD
      <section
        className="
        max-w-7xl
        mx-auto
        py-16
        px-6
    "
      >
        <h2
          className="
            text-4xl
            font-bold
            text-center
            mb-12
        "
        >
          Sản phẩm nổi bật
        </h2>
=======
      <CategoryMenu
        onSelectCategory={handleSelectCategory}
      />

      {/* Products */}

      <section className="max-w-7xl mx-auto py-16 px-6">

        <div className="flex justify-between items-center mb-10">
          <h2 className="text-4xl font-bold">
            Sản phẩm nổi bật
          </h2>

          <Link
            to="/products"
            className="
              bg-emerald-500
              text-white
              px-5
              py-3
              rounded-lg
              hover:bg-emerald-600
              transition
            "
          >
            Xem tất cả
          </Link>
        </div>
>>>>>>> 6deb3d7 (cap nhat them frontend)

        <div
          className="
            grid
            grid-cols-1
<<<<<<< HEAD
            md:grid-cols-2
            lg:grid-cols-4
            gap-8
        "
        >
          {featuredProducts.map((product) => (
            <ProductCard key={product.id} product={product} />
          ))}
        </div>
      </section>
      <Link
        to="/products"
        className="
        bg-emerald-500
        text-white
        px-6
        py-3
        rounded-lg
    "
      >
        Xem tất cả sản phẩm
      </Link>
=======
            sm:grid-cols-2
            lg:grid-cols-4
            gap-8
          "
        >
          {featuredProducts.map((product) => (
            <ProductCard
              key={product.id}
              product={product}
            />
          ))}
        </div>
      </section>

      {/* Services */}

      <section className="bg-gray-100 py-14">
        <div className="max-w-7xl mx-auto px-6">

          <div className="grid md:grid-cols-4 gap-6">

            <div className="bg-white p-6 rounded-xl shadow">
              <h3 className="font-bold mb-2">
                🚚 Giao hàng toàn quốc
              </h3>
              <p className="text-gray-500">
                Giao hàng nhanh từ 2-5 ngày.
              </p>
            </div>

            <div className="bg-white p-6 rounded-xl shadow">
              <h3 className="font-bold mb-2">
                💳 Thanh toán an toàn
              </h3>
              <p className="text-gray-500">
                Hỗ trợ nhiều phương thức thanh toán.
              </p>
            </div>

            <div className="bg-white p-6 rounded-xl shadow">
              <h3 className="font-bold mb-2">
                🔄 Đổi trả dễ dàng
              </h3>
              <p className="text-gray-500">
                Đổi trả trong vòng 7 ngày.
              </p>
            </div>

            <div className="bg-white p-6 rounded-xl shadow">
              <h3 className="font-bold mb-2">
                🎁 Nhiều ưu đãi
              </h3>
              <p className="text-gray-500">
                Giảm giá liên tục mỗi tháng.
              </p>
            </div>

          </div>
        </div>
      </section>

      {/* Posts */}

      <LatestPosts posts={posts} />

      {/* Promo Banner */}

      <section className="py-16">
        <div
          className="
            max-w-7xl
            mx-auto
            px-6
          "
        >
          <div
            className="
              bg-gradient-to-r
              from-emerald-500
              to-teal-600
              text-white
              rounded-2xl
              p-12
              text-center
            "
          >
            <h2 className="text-4xl font-bold mb-4">
              Giảm giá đến 50%
            </h2>

            <p className="mb-6">
              Bộ sưu tập thời trang mới nhất
            </p>

            <Link
              to="/products"
              className="
                bg-white
                text-emerald-600
                px-6
                py-3
                rounded-lg
                font-semibold
              "
            >
              Mua ngay
            </Link>
          </div>
        </div>
      </section>

>>>>>>> 6deb3d7 (cap nhat them frontend)
      <Footer />
    </>
  );
}

<<<<<<< HEAD
export default Home;
=======
export default Home;
>>>>>>> 6deb3d7 (cap nhat them frontend)
