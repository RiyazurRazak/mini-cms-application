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

export const getComments = () => axios.get(`${baseUrl}/hyper/comments`)

export const deleteComment = (id) => axios.delete(`${baseUrl}/hyper/comment/${id}`)

export const getUsers = () => axios.get(`${baseUrl}/hyper/comment-users`)

export const addPage = (payload) => axios.post(`${baseUrl}/hyper/page`, payload)

export const getPages = () => axios.get(`${baseUrl}/hyper/pages`)

export const changePageVisibility = (id) => axios.put(`${baseUrl}/hyper/page/status/${id}`)

export const deletePage = (id) => axios.delete(`${baseUrl}/hyper/page/${id}`)

export const getCommentStatus = () => axios.get(`${baseUrl}/hyper/stats/comments`)
