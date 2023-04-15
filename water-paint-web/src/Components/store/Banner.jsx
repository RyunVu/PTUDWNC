import Carousel from 'react-bootstrap/Carousel';
import Banner1 from '../../assets/images/banner1.jpg';
import Banner2 from '../../assets/images/banner2.jpg';
import Banner3 from '../../assets/images/banner3.jpg';

function Banner() {
    return (
        <Carousel>
            <Carousel.Item interval={3000}>
                <img className="d-block w-100" src={Banner1} alt="banner 1" />
            </Carousel.Item>
            <Carousel.Item interval={3000}>
                <img className="d-block w-100" src={Banner2} alt="banner 2" />
            </Carousel.Item>
            <Carousel.Item interval={3000}>
                <img className="d-block w-100" src={Banner3} alt="banner 3" />
            </Carousel.Item>
        </Carousel>
    );
}

export default Banner;
