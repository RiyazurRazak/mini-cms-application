import axios from 'axios'
import { baseUrl } from '../../constants/constants'

export const getBrandDetails = () => axios.get(`${baseUrl}/hyper/meta`)

export const updateBrandDetails = (payload) => axios.put(`${baseUrl}/hyper/meta`, payload)

export const updateTheme = (payload) => axios.put(`${baseUrl}/hyper/theme`, payload)

export const getRootUsers = () => axios.get(`${baseUrl}/hyper/users`)

export const addUser = (payload) => axios.post(`${baseUrl}/hyper/user`, payload)

export const deleteUser = (id) => axios.delete(`${baseUrl}/hyper/user/${id}`)

export const requestMfa = () => axios.get(`${baseUrl}/hyper/enable-2fa`)

export const verifyMfa = (code) => axios.put(`${baseUrl}/hyper/verify-2fa/${code}`)

export const getBlogs = () => axios.get(`${baseUrl}/hyper/blogs`)

export const createBlog = (payload) => axios.post(`${baseUrl}/hyper/blog`, payload)

export const changeBlogVisibility = (id) => axios.put(`${baseUrl}/hyper/blog/status/${id}`)

export const getBlog = (id) => axios.get(`${baseUrl}/blogs/article/${id}`)

export const editBlog = (id, payload) => axios.put(`${baseUrl}/hyper/blog/${id}`, payload)

export const deleteBlog = (id) => axios.delete(`${baseUrl}/hyper/blog/${id}`)
