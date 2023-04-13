import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

import { StoreHome, StoreLayout, ContactPage, CategoryPage } from './Pages';

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
                </Routes>
            </Router>
        </div>
    );
}

export default App;
