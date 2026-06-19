import { useEffect, useState } from "react";
import { getProducts } from "../api/productApi";

import Header from "../components/Header";
import Banner from "../components/Banner";
import ProductCard from "../components/products/ProductCard";
import Footer from "../components/Footer";
import { Link } from "react-router-dom";

function Home() {
  const [products, setProducts] = useState([]);

  useEffect(() => {
    loadProducts();
  }, []);

  const loadProducts = async () => {
    const data = await getProducts();
    setProducts(data);
  };
  const featuredProducts = products.slice(0, 4);
  return (
    <>
      <Header />

      <Banner />

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

        <div
          className="
            grid
            grid-cols-1
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
      <Footer />
    </>
  );
}

export default Home;
