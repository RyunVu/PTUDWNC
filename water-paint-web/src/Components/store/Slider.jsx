import Carousel from 'react-bootstrap/Carousel';
import Image1 from '../../assets/images/banner1.jpg';
import Image2 from '../../assets/images/banner2.jpg';
import Image3 from '../../assets/images/banner3.jpg';

function Slider() {
    return (
        <Carousel>
            <Carousel.Item interval={3000}>
                <img
                    className="d-block w-100"
                    src={Image1}
                    alt="banner 1"
                />
            </Carousel.Item>
            <Carousel.Item interval={3000}>
                <img
                    className="d-block w-100"
                    src={Image2}
                    alt="banner 2"
                />
            </Carousel.Item>
            <Carousel.Item interval={3000}>
                <img
                    className="d-block w-100"
                    src={Image3}
                    alt="banner 3"
                />
            </Carousel.Item>
        </Carousel>
    );
}

export default Slider;
