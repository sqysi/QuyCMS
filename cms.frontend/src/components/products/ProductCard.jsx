function ProductCard({ product }) {

    const imageUrl =
        `https://localhost:7108${product.imageUrl}`;

    return (
        <div
            className="
                bg-white
                rounded-2xl
                overflow-hidden
                shadow-md
                hover:shadow-2xl
                hover:-translate-y-2
                transition
                duration-300
            "
        >
            <img
                src={imageUrl}
                alt={product.name}
                className="
                    h-64
                    w-full
                    object-cover
                "
            />

            <div className="p-4">

                <h3 className="
                    font-semibold
                    text-lg
                    mb-2
                ">
                    {product.name}
                </h3>

                <p className="
                    text-emerald-600
                    text-xl
                    font-bold
                ">
                    {product.price.toLocaleString("vi-VN")}₫
                </p>

                <button
                    className="
                        mt-4
                        w-full
                        bg-emerald-500
                        text-white
                        py-3
                        rounded-xl
                        hover:bg-emerald-600
                    "
                >
                    Thêm vào giỏ
                </button>

            </div>
        </div>
    );
}

export default ProductCard;