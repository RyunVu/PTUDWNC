import { Outlet } from 'react-router-dom';
import Navbar from '../../Components/admin/Navbar';
import Footer from '../../Components/store/Footer';

const AdminLayout = () => {
    return (
        <>
            <Navbar />
            <div className="container-fluid py-3">
                <Outlet />
            </div>
            <Footer />
        </>
    );
};

export default AdminLayout;
