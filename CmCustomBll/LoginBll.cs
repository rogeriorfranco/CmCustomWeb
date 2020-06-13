using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Novell.Directory.Ldap;
using CmCustomDto;
using CmCustomDal;
using System.IO;

namespace CmCustomBll
{
    public class LoginBll
    {
        private static User usuarioLdap = null;        
        public User autentication(string user, string password)
        {
            if (string.IsNullOrEmpty(user))
                throw new Exception("Favor informar o usuário");

            if (string.IsNullOrEmpty(password))
                throw new Exception("Favor informar a senha");

            LdapConnection ldap = new LdapConnection();
            ldap.Connect("ldapctbc.network.ctbc", 389);

            if (ldap.Connected)
            {
                try
                {
                    usuarioLdap = new User();
                    ldap.Bind("cn=BIND_CONNECT_MASTER,ou=especial,cn=users,dc=network,dc=ctbc", "C0nn3ct3M@st3rW3b");

                    if (checkAccess("cn=USUARIO,cn=CONNECT_MASTER_WEB,cn=PERFIS,dc=network,dc=ctbc", user, password, ldap))
                        return usuarioLdap;

                    if (checkAccess("cn=LEITURA,cn=CONNECT_MASTER_WEB,cn=PERFIS,dc=network,dc=ctbc", user, password, ldap))
                        return usuarioLdap;

                    if (checkAccess("cn=ADMINISTRADOR_REDE,cn=CONNECT_MASTER_WEB,cn=PERFIS,dc=network,dc=ctbc", user, password, ldap))
                        return usuarioLdap;

                    if (checkAccess("cn=ADMINISTRADOR_TI,cn=CONNECT_MASTER_WEB,cn=PERFIS,dc=network,dc=ctbc", user, password, ldap))
                        return usuarioLdap;

                    if (usuarioLdap.autenticado == false)
                        throw new Exception("Usuário sem acesso, favor solicitar no CAL");
                }
                catch (LdapException e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    ldap.Disconnect();
                }
            }
            return usuarioLdap;
        }

        private bool checkAccess(string dnPerfil, string user, string password, LdapConnection ldap)
        {
            try
            {
                if (usuarioLdap.autenticado == false)
                {
                    LdapSearchResults search = ldap.Search(dnPerfil, LdapConnection.SCOPE_SUB, null, null, false);
                    LdapEntry entry = search.next();
                    LdapAttributeSet attribute = entry.getAttributeSet();
                    LdapAttribute attrib = attribute.getAttribute("uniquemember");

                    //if (attrib != null)
                    //{                        
                    //    usuarioLdap.autenticado = Array.Exists(attrib.StringValueArray, element => element.Contains(user.ToLower()));
                    //    if (usuarioLdap.autenticado)
                    //        usuarioLdap.perfil = attribute.getAttribute("cn").StringValue;
                    //    return usuarioLdap.autenticado;
                    //}

                    foreach (string usuario in attrib.StringValueArray)
                    {                        
                        if (usuario.Substring(3).Split(',').First().Equals(user))
                        {
                            ldap.Bind(usuario, password);

                            LdapSearchResults searchu = ldap.Search("cn=users,dc=network,dc=ctbc", LdapConnection.SCOPE_SUB, "uid=" + user, null, false);
                            LdapEntry entryu = searchu.next();
                            LdapAttributeSet attributeu = entryu.getAttributeSet();

                            usuarioLdap.CPF = attributeu.getAttribute("CPF").StringValue;
                            usuarioLdap.nomeAssociado = attributeu.getAttribute("DISPLAYNAME").StringValue;
                            usuarioLdap.centroResultado = attributeu.getAttribute("DEPARTMENTNUMBER").StringValue;
                            usuarioLdap.email = attributeu.getAttribute("MAIL").StringValue;
                            usuarioLdap.tipo_usuario = attributeu.getAttribute("TIPOUSUARIO").StringValue;
                            usuarioLdap.usuario = attributeu.getAttribute("CN").StringValue.ToLower();

                            usuarioLdap.perfil = attribute.getAttribute("cn").StringValue;
                            usuarioLdap.autenticado = true;
                            return true;
                        }
                    }

                }
            }
            catch (LdapException)
            {
                throw new Exception("Usuário ou Senha Incorreta!");
            }

            return false;
        }


    }
}
