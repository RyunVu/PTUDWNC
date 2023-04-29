const SET_USER_ID = 'set_user_id';

const SET_ROLES = 'set_roles';
const setRoles = (payload) => ({
    type: SET_ROLES,
    payload,
});

const setUserId = (payload) => ({
    type: SET_USER_ID,
    payload,
});

export { setRoles, setUserId };
