import { Outlet } from 'react-router-dom';

import Navbar from '../../Components/store/Navbar';
import Footer from '../../Components/store/Footer';
import GlobalStyle from '../../Components/store/GlobalStyle';

export default function Layout() {
    return (
        <>
            <GlobalStyle>
                <Navbar />
                <Outlet />
                <Footer />
            </GlobalStyle>
        </>
    );
}
