import { Outlet } from 'react-router-dom';
import Navbar from '../../Components/admin/Navbar';
import Footer from '../../Components/store/Footer';
import Sidebar from '../../Components/admin/Sidebar/sidebar';

const AdminLayout = () => {
    return (
        <>
            {/* <Navbar /> */}
            <Sidebar />
            {/* <div className="container-fluid py-3">
                <Outlet />
            </div> */}
            {/* <Footer /> */}
        </>
    );
};

export default AdminLayout;
