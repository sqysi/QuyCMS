import { useEffect, useState } from "react";
import { getProducts } from "../api/productApi";

import Header from "../components/Header";
import Banner from "../components/Banner";
import ProductCard from "../components/products/ProductCard";
import Footer from "../components/Footer";

function Home() {
  const [products, setProducts] = useState([]);

  useEffect(() => {
    loadProducts();
  }, []);

  const loadProducts = async () => {
    const data = await getProducts();
    setProducts(data);
  };

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
          {products.map((product) => (
            <ProductCard key={product.id} product={product} />
          ))}
        </div>
      </section>

      <Footer />
    </>
  );
}

export default Home;
