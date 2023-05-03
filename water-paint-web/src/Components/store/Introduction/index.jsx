import React from 'react';
import { Col, Container, Row } from 'react-bootstrap';
import IntroductionImage from '../../../assets/images/IntroductionImage.png';
import styles from './Introduction.module.scss';

function Introduction() {
    return (
        <Container className="mt-4 mb-4">
            <h2 className={styles.header}>Thư ngỏ</h2>
            <h5 className={styles.subheader}>Kính gửi Quý khách hàng thân mến!</h5>
            <Row className="mb-2">
                <Col xs={8}>
                    <p className={styles.para}>
                        &nbsp;&nbsp;&nbsp;&nbsp;Nếu được hỏi điều gì tạo nên sự khác biệt và là nền tảng trong hơn 20
                        năm hình thành và phát triển của KOVA, thì đó chỉ có thể là "Nhà khoa học làm kinh doanh", khi
                        chủ doanh nghiệp biến niềm đam mê và trách nhiệm của một nhà khoa học trở thành những thành tựu
                        có ích và phục vụ lợi ích kinh tế cho cộng đồng và xã hội. Bắt nguồn từ khát vọng phi thường
                        nhưng không thể viễn vông - "đưa thương hiệu sơn KOVA Việt Nam trở thành thương hiệu được công
                        nhận trên thế giới" - thương hiệu KOVA cứ thế mà vượt qua bao khó khăn chỉ để khẳng định một
                        điều duy nhất, chất lượng sản phẩm và những bước tiến đột phá trong tính năng sản phẩm. Đến nay
                        KOVA tự hào trở thành thương hiệu có thể mang đến cho Quý khách hàng những sản phẩm sơn và vật
                        liệu chống thấm có chất lượng ngang tầm, thậm chí vượt trội so với những nền công nghiệp sơn
                        hàng đầu thế giới. Nguyên nhân chủ yếu là do sản phẩm của KOVA đã hoàn toàn nhiệt đới hóa - phù
                        hợp với khí hậu nhiệt đới của Việt Nam và các nước trong khu vực.
                    </p>
                </Col>
                <Col xs={4}>
                    <img className={styles.image} src={IntroductionImage} alt="introduction" />
                </Col>
            </Row>
            <p className={styles.para}>
                &nbsp;&nbsp;&nbsp;&nbsp;Cho đến nay, Tập đoàn Sơn KOVA đã không ngừng lớn mạnh, khẳng định được vị thế
                của mình trong lĩnh vực sơn và chống thấm ở thị trường trong nước cũng như quốc tế, đặc biệt là thị
                trường Singapore - một thị trường rất cạnh tranh với những đòi hỏi rất cao về chất lượng sản phẩm. Nhận
                biết được thế mạnh của mình, mỗi thành viên của KOVA từ nhân viên đến lãnh đạo Tập đoàn luôn cam kết
                không ngừng học tập và làm việc nghiêm túc để có thể giữ vững cũng như nâng cao hơn nữa chất lượng và sự
                đa dạng sản phẩm của KOVA. Qua đó có thể mang đến cho Quý khách hàng những không gian được trang trí
                sang trọng mà gần gũi, với vẻ đẹp và độ bền cao nhất.
            </p>
            <p className={styles.para}>
                &nbsp;&nbsp;&nbsp;&nbsp;Với những thành công bước đầu cùng nền tảng vững chắc về kỹ thuật và công nghệ,
                đặc biệt là sự tin tưởng và yêu mến của Quý khách hàng, Hội đồng Quản trị, Ban Giám đốc và toàn thể cán
                bộ công nhân viên Tập đoàn tin tưởng KOVA sẽ ngày càng phát triển, trở thành một trong những thương hiệu
                hàng đầu tại VIệt Nam.
            </p>
            <p className={styles.para}>
                &nbsp;&nbsp;&nbsp;&nbsp;Thay mặt Tập đoàn Sơn KOVA, tôi xin gửi đến Quý khách hàng lời cảm ơn và lời
                chúc sức khỏe chân thành nhất.
            </p>
            <div className={styles.signature}>
                <p className={styles.signatureGroup}>Chủ tịch Tập đoàn Sơn KOVA</p>
                <p className={styles.chairperson}>PGS.TS Nguyễn Thị Hòe</p>
            </div>
        </Container>
    );
}

export default Introduction;
