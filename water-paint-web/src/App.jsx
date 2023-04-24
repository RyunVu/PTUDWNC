import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

import {
    BadRequest,
    NotFound,
    StoreHome,
    StoreLayout,
    ContactPage,
    CategoryPage,
    AdminHome,
    AdminLayout,
    Categories,
    Products,
    Units,
    Accounts,
    ProductEdit,
    UnitEdit,
    AccountEdit,
    CategoryEdit,
} from './Pages';

function App() {
    return (
        <div>
            <Router>
                <Routes>
                    <Route path="/" element={<StoreLayout />}>
                        <Route path="/" element={<StoreHome />} />
                        <Route path="store" element={<StoreHome />} />
                        <Route path="store/category" element={<CategoryPage />} />
                        <Route path="store/contact" element={<ContactPage />} />
                    </Route>

                    <Route path="/admin" element={<AdminLayout />}>
                        <Route path="/admin" element={<AdminHome />} />

                        <Route path="/admin/accounts" element={<Accounts />} />
                        <Route path="/admin/accounts/edit" element={<AccountEdit />} />
                        <Route path="/admin/accounts/edit/:id" element={<AccountEdit />} />

                        <Route path="/admin/categories" element={<Categories />} />
                        <Route path="/admin/categories/edit" element={<CategoryEdit />} />
                        <Route path="/admin/categories/edit/:id" element={<CategoryEdit />} />

                        <Route path="/admin/products" element={<Products />} />
                        <Route path="/admin/products/edit" element={<ProductEdit />} />
                        <Route path="/admin/products/edit/:id" element={<ProductEdit />} />

                        <Route path="/admin/products/edit/:id/units/" element={<Units />} />
                        <Route path="/admin/products/edit/:id/units" element={<UnitEdit />} />
                        <Route path="/admin/products/edit/:id/units/:id" element={<UnitEdit />} />
                    </Route>

                    <Route path="/400" element={<BadRequest />} />
                    <Route path="*" element={<NotFound />} />
                </Routes>
            </Router>
        </div>
    );
}

export default App;
