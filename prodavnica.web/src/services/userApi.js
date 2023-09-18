import axios from "axios";

const API_URL = process.env.APP_URL;
const USER_PATH = "/users";

const api = axios.create({
    baseURL: API_URL + USER_PATH,
});

const apiPost = async (operation, data) => {
    return api.post(operation, data);
};

const apiGet = async (operation, data) => {
    return api.get(operation, data);
};

const apiPut = async (operation, data) => {
    return api.put(operation, data);
};

const RegisterUser = async(data) => {
    return apiPost("", data);
}

const UpdateProfile = async(data) => {
    return apiPut("?id=" + data.id, data);
}

const UserApi = {
    RegisterUser,
    UpdateProfile,
}

export default UserApi;