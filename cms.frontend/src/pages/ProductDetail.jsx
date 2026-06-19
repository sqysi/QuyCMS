import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

import Header from "../components/Header";
import Footer from "../components/Footer";

import { getProductById } from "../api/productApi";

function ProductDetail() {

    const { id } = useParams();

    const [product, setProduct] = useState(null);

    useEffect(() => {
        loadProduct();
    }, [id]);

    const loadProduct = async () => {
        const data =
            await getProductById(id);

        setProduct(data);
    };

    if (!product) {
        return (
            <div className="text-center mt-20">
                Đang tải...
            </div>
        );
    }

    return (
        <>
            <Header />

            <div className="
                max-w-7xl
                mx-auto
                py-10
                px-6
            ">
                <div className="
                    grid
                    md:grid-cols-2
                    gap-12
                ">

                    <div>

                        <img
                            src={`https://localhost:7108${product.imageUrl}`}
                            alt={product.name}
                            className="
                                w-full
                                rounded-xl
                                shadow-lg
                            "
                        />

                    </div>

                    <div>

                        <h1 className="
                            text-4xl
                            font-bold
                            mb-4
                        ">
                            {product.name}
                        </h1>

                        <p className="
                            text-emerald-600
                            text-3xl
                            font-bold
                            mb-6
                        ">
                            {product.price.toLocaleString("vi-VN")} ₫
                        </p>

                        <p className="
                            text-gray-600
                            mb-6
                        ">
                            Tồn kho:
                            {" "}
                            {product.stockQuantity}
                        </p>

                        <div className="
                            border-t
                            pt-6
                        ">
                            <h3 className="
                                text-xl
                                font-semibold
                                mb-3
                            ">
                                Mô tả sản phẩm
                            </h3>

                            <p>
                                {product.description}
                            </p>
                        </div>

                        <button
                            className="
                                mt-8
                                bg-emerald-500
                                text-white
                                px-8
                                py-4
                                rounded-xl
                                hover:bg-emerald-600
                            "
                        >
                            Thêm vào giỏ hàng
                        </button>

                    </div>

                </div>
            </div>

            <Footer />
        </>
    );
}

export default ProductDetail;