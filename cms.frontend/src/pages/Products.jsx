import { useEffect, useState } from "react";

import Header from "../components/Header";
import Footer from "../components/Footer";

import ProductCard from "../components/products/ProductCard";

import { getProducts } from "../api/productApi";

function Products() {

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

            <div className="max-w-7xl mx-auto py-10">

                <h1 className="text-4xl font-bold mb-8">
                    Danh sách sản phẩm
                </h1>

                <div className="
                    grid
                    grid-cols-1
                    md:grid-cols-2
                    lg:grid-cols-4
                    gap-8
                ">
                    {
                        products.map(product => (
                            <ProductCard
                                key={product.id}
                                product={product}
                            />
                        ))
                    }
                </div>

            </div>

            <Footer />
        </>
    );
}

export default Products;