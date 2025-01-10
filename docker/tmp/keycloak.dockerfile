# Estágio intermediário: usar uma imagem mais completa para instalar pacotes
FROM registry.access.redhat.com/ubi9 AS build
RUN mkdir -p /mnt/rootfs && \
    dnf install --installroot /mnt/rootfs curl --releasever 9 --setopt install_weak_deps=false --nodocs -y && \
    dnf --installroot /mnt/rootfs clean all && \
    rpm --root /mnt/rootfs -e --nodeps setup

# Estágio final: usar a imagem oficial do Keycloak
FROM quay.io/keycloak/keycloak:26.0.7

# Copiar o `curl` instalado do estágio anterior
COPY --from=build /mnt/rootfs /

# Configurar as variáveis de ambiente necessárias para o Keycloak
ENV KC_HEALTH_ENABLED=true
ENV KC_METRICS_ENABLED=true

# Expor as portas do Keycloak
EXPOSE 8080 7443 9000

# Definir o comando de inicialização
ENTRYPOINT ["/opt/keycloak/bin/kc.sh"]
CMD ["start-dev", "--http-port", "8080"]
