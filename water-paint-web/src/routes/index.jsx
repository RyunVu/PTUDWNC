import {
    StoreHome,
    StoreShop,
    ContactPage,
    CategoryPage,
    AdminHome,
    Categories,
    Products,
    // Units,
    Accounts,
    ProductEdit,
    UnitEdit,
    AccountEdit,
    CategoryEdit,
} from '../Pages';

const publicRoutes = [
    { path: '/', component: StoreHome },
    { path: '/store', component: StoreShop },
    { path: '/store/category', component: CategoryPage },
    { path: '/store/contact', component: ContactPage },
];
const privateRoute = [
    { path: '/admin', component: AdminHome },
    { path: '/admin/accounts', component: Accounts },
    { path: '/admin/accounts/edit', component: AccountEdit },
    { path: '/admin/accounts/edit/:id', component: AccountEdit },
    { path: '/admin/categories', component: Categories },
    { path: '/admin/categories/edit', component: CategoryEdit },
    { path: '/admin/categories/edit/:id', component: CategoryEdit },
    { path: '/admin/products', component: Products },
    { path: '/admin/products/edit', component: ProductEdit },
    { path: '/admin/products/edit/:id', component: ProductEdit },
    { path: '/admin/products/:productId/add/units', component: UnitEdit },
    { path: '/admin/products/edit/:id/units/:id', component: UnitEdit },
];

export { publicRoutes, privateRoute };
