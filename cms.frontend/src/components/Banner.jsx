function Banner() {
    return (
        <section
            className="
                h-[600px]
                bg-cover
                bg-center
                flex
                items-center
                justify-center
            "
            style={{
                backgroundImage:
                    "url('https://images.unsplash.com/photo-1441986300917-64674bd600d8')"
            }}
        >
            <div className="text-center text-white">
                <h1 className="
                    text-6xl
                    font-bold
                    mb-4
                ">
                    CMS Fashion
                </h1>

                <p className="text-2xl">
                    Bộ sưu tập mới nhất 2026
                </p>

                <button className="
                    mt-8
                    bg-emerald-500
                    px-8
                    py-4
                    rounded-full
                ">
                    Mua ngay
                </button>
            </div>
        </section>
    );
}

export default Banner;