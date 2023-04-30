import axios from 'axios'
import { baseUrl } from '../../constants/constants'

export const getBrandDetails = () => axios.get(`${baseUrl}/hyper/meta`)

export const updateBrandDetails = (payload) => axios.put(`${baseUrl}/hyper/meta`, payload)

export const updateTheme = (payload) => axios.put(`${baseUrl}/hyper/theme`, payload)

export const getRootUsers = () => axios.get(`${baseUrl}/hyper/users`)

export const addUser = (payload) => axios.post(`${baseUrl}/hyper/user`, payload)

export const deleteUser = (id) => axios.delete(`${baseUrl}/hyper/user/${id}`)
