import { axios } from '@/libs/api.request'
export const GetAuth = () => {
  return axios.get('api/RBAC/GetAuth')
}
export const DeleteAuth = id => {
  return axios.delete('api/RBAC/DeleteAuth/' + id)
}
export const SaveAuth = form => {
  return axios.post('api/RBAC/SaveAuth', form)
}

export const GetRole = () => {
  return axios.get('api/RBAC/GetRole')
}
export const DeleteRole = id => {
  return axios.delete('api/RBAC/DeleteRole/' + id)
}
export const SaveRole = form => {
  return axios.post('api/RBAC/SaveRole', form)
}
export const GetAuthByRoleId = id => {
  return axios.get('api/RBAC/GetAuthByRoleId/' + id)
}

export const GiveRoleSecurity = form => {
  return axios.post('api/RBAC/GiveRoleSecurity', form)
}

export const SearchEmployee = form => {
  return axios.post('api/RBAC/SearchEmployee', form)
}

export const SaveEmployee = form => {
  return axios.post('api/RBAC/SaveEmployee', form)
}

export const DeleteEmployee = id => {
  return axios.delete('api/RBAC/DeleteEmployee/' + id)
}
export const GetRoleByUserId = id => {
  return axios.get('api/RBAC/GetRoleByUserId/' + id)
}

export const GiveUserRole = form => {
  return axios.post('api/RBAC/GiveUserRole', form)
}

// #region 密码管理

export const UpdatePwd = form => {
  return axios.post('api/RBAC/UpdatePwd', form)
}

export const ResetPwd = id => {
  return axios.post('api/RBAC/ResetPwd/' + id)
}
